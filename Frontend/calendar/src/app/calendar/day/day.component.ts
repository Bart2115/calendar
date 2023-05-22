import { Component, Input, OnChanges } from '@angular/core';
import { DateService } from 'src/app/services/date.service';

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.css']
})
export class DayComponent implements OnChanges{
  @Input() id = "";
  displayDate = "";
  isCurrentMonth = false;
  isToday = false;
  @Input() isSelected = false;

  constructor(private readonly dateService: DateService){}

  ngOnChanges(){
    this.displayDate = (this.id.split("/")[1]).replace(/^0+/, '');
    var month = (this.id.split("/")[0]).replace(/^0+/, '');
    var today = this.dateService.convertDate(this.dateService.todayDate);
    var currentMonth = (this.dateService.currentDate.getMonth() + 1).toString();
    if(month == currentMonth) this.isCurrentMonth = true;
    if(today == this.id) this.isToday = true;
  }

  showNote(){
    this.dateService.sendNoteDayMessage(this.id);
  }
}
