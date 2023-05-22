import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { NoteInfo } from "../models/note-info.model";

@Injectable({ providedIn: 'root' })
export class NoteService{
    private backendUrl = "https://localhost:5001/api/Notes";

    constructor(private httpClient: HttpClient){}

    public getNotes(date: string){
        return this.httpClient.get<NoteInfo[]>(this.backendUrl + `?noteDate=${date}`);
    }

}