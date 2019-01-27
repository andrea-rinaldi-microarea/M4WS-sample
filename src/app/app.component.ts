import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private _httpService: Http) { }
   isAlive: boolean = false;
   ngOnInit() {
      this._httpService.get('/api/loginManager/isAlive').subscribe(values => {
         this.isAlive = values.json() as boolean;
      });
   }
}
