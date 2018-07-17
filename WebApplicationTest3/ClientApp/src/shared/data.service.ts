import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders  } from "@angular/common/http";
import { Observable } from "rxjs";
import { friend } from "./friend"

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class DataService {

    constructor(private http: HttpClient) {

    }

    getFriends(): Observable<any>
    {
        return this.http.get('GetAll');
    }
}