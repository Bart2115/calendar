import { Component, OnDestroy } from '@angular/core';
import { DateService } from '../services/date.service';
import { MonthWeeks } from '../models/month-weeks.model';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnDestroy{
  private readonly destroyed$ = new Subject();
  daysShortcuts = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
  monthWeeks = new MonthWeeks();
  selectedDate = "";

  constructor(private readonly dateService: DateService){
    this.monthWeeks = dateService.setCalendar();
    dateService.selectedDateToShow$.pipe(takeUntil(this.destroyed$))
      .subscribe( () =>
        this.monthWeeks = dateService.setCalendar()
    );
    dateService.eventDayMessage$.pipe(takeUntil(this.destroyed$))
      .subscribe( message =>
        this.selectedDate = message
    );
  }

  ngOnDestroy(){
    this.destroyed$.next("");
    this.destroyed$.complete();
  }
}
