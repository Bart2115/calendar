import { Subject } from 'rxjs';
import { OnDestroy, Injectable } from '@angular/core';
import { MonthWeeks } from '../models/month-weeks.model';

@Injectable({ providedIn: 'root' })
export class DateService implements OnDestroy{
  private monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"];
  selectedDateToShow$ = new Subject<Date>();
  currentDate = new Date();
  todayDate = new Date();
  lastSelectedDate = "";
  noteDayMessage$ = new Subject<string>();

  ngOnDestroy(){
    this.selectedDateToShow$.complete();
    this.noteDayMessage$.complete();
  }

  sendPreviousDate(){
    this.currentDate.setMonth(this.currentDate.getMonth() - 1);
    this.selectedDateToShow$.next(this.currentDate);
    if(this.lastSelectedDate) this.noteDayMessage$.next(this.lastSelectedDate);
  }

  sendNextDate(){
    this.currentDate.setMonth(this.currentDate.getMonth() + 1);
    this.selectedDateToShow$.next(this.currentDate);
    if(this.lastSelectedDate) this.noteDayMessage$.next(this.lastSelectedDate);
  }

  getShowDate(){
    return this.monthNames[this.currentDate.getMonth()] + " " + this.currentDate.getFullYear();
  }

  setCalendar(){
    var monthWeeks = new MonthWeeks();
    this.todayDate = new Date();
    var firstDayOfWeek = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), 1).getDay();
    var currentDay = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), 1);

    //set first week
    var daysToSkip = firstDayOfWeek - 1;
    if (daysToSkip == -1) daysToSkip = 6;
    currentDay.setDate(currentDay.getDate() - daysToSkip);
    for(let i = 0; i < daysToSkip; i++){
      monthWeeks.firstWeek.push(this.convertDate(currentDay));
      currentDay.setDate(currentDay.getDate() + 1);
    }
    for(let i = 7 - daysToSkip; i>0; i--){
      monthWeeks.firstWeek.push(this.convertDate(currentDay));
      currentDay.setDate(currentDay.getDate() + 1);
    }
    //set rest weeks
    for(let i = 7; i>0; i--){
      monthWeeks.secondWeek.push(this.convertDate(currentDay));
      currentDay.setDate(currentDay.getDate() + 7);
      monthWeeks.thirdWeek.push(this.convertDate(currentDay));
      currentDay.setDate(currentDay.getDate() + 7);
      monthWeeks.fourthWeek.push(this.convertDate(currentDay));
      currentDay.setDate(currentDay.getDate() + 7);
      monthWeeks.fifthWeek.push(this.convertDate(currentDay));
      currentDay.setDate(currentDay.getDate() + 7);
      monthWeeks.sixthWeek.push(this.convertDate(currentDay));
      currentDay.setDate(currentDay.getDate() - 27);
    }
    return monthWeeks;
  }

  convertDate(date: Date){
    return date.toLocaleDateString(undefined, {year: 'numeric', month: '2-digit', day: '2-digit'});
  }

  sendNoteDayMessage(message: string){
    this.noteDayMessage$.next(message);
    this.lastSelectedDate = message;
  }
}
