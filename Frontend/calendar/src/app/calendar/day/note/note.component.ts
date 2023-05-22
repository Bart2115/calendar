import { Component, OnDestroy, OnInit} from '@angular/core';
import { NoteInfo } from '../../../models/note-info.model';
import { DateService } from 'src/app/services/date.service';
import { Subject, takeUntil, take } from 'rxjs';
import { NoteService } from 'src/app/services/note.service';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit, OnDestroy{
  private readonly destroyed$ = new Subject();
  notes: NoteInfo[] = [];

  constructor(private readonly dateService: DateService, private readonly noteService: NoteService){}

  ngOnInit(){
    this.dateService.noteDayMessage$.pipe(takeUntil(this.destroyed$))
      .subscribe( message => {
        this.getNoteInfo(message);
    })
  }

  ngOnDestroy(){
    this.destroyed$.next("");
    this.destroyed$.complete();
  }

  private getNoteInfo(id: string){
    this.notes = [];
    this.noteService.getNotes(id).pipe(take(1)).subscribe( (notes: NoteInfo[]) => {
      notes.forEach(note => this.notes.push(note))
      console.log(notes)
    })
  }
}
