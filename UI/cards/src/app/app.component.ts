import {Component, OnInit} from '@angular/core';
import {CardsService} from "./service/cards.service";
import {Card} from "./models/card.model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'cards';
  cards: Card[] = [];

  constructor(private cardsService: CardsService) {

  }

  ngOnInit(): void {
    this.getAllCards();
  }

  getAllCards() {
    this.cardsService.getAllCards()
      .subscribe(
        response => {
          this.cards = response;
        }
      );
  }
}
