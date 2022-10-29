import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Card} from "../models/card.model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  baseUrl = 'https://localhost:7033/api/cards';
  constructor(private http: HttpClient) { }

  // Get all cards
  getAllCards() : Observable<Card[]> {
    return this.http.get<Card[]>(this.baseUrl);
  }
}
