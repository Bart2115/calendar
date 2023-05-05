import { Component } from '@angular/core';
import { DateService } from '../services/date.service';

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.css']
})
export class MainMenuComponent{
  showDate = "";

  constructor(private date: DateService){
    this.showDate = date.getShowDate();
    date.selectedDateToShow$.subscribe( () =>
      this.showDate = date.getShowDate()
    );
  }

  previousMonth(){
    this.date.sendPreviousDate();
  }

  nextMonth(){
    this.date.sendNextDate();
  }

}
