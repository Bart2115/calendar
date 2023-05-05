import { Component, OnDestroy, OnInit} from '@angular/core';
import { EventInfo } from '../../../models/event-info.model';
import { DateService } from 'src/app/services/date.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit, OnDestroy{
  private readonly destroyed$ = new Subject();
  events: EventInfo[] = [];

  constructor(private readonly dateService: DateService){}

  ngOnInit(){
    this.dateService.eventDayMessage$.pipe(takeUntil(this.destroyed$))
      .subscribe( message => {
        this.getEventInfo(message);
    })
  }

  ngOnDestroy(){
    this.destroyed$.next("");
    this.destroyed$.complete();
  }

  private getEventInfo(id: string){
    this.events = [];
    //request to repository
    var eventInfo = new EventInfo();
    eventInfo.id = id;
    eventInfo.info = "test";
    eventInfo.type = "info";
    this.events.push(eventInfo);
  }
}
