import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

enum ConnectionStatus { connecting, connected, noConnection }
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private _httpService: Http) { }
  connStatus: ConnectionStatus = ConnectionStatus.connecting;
  connectionStatus = ConnectionStatus;
   ngOnInit() {
      this._httpService.get('/api/loginManager/isAlive').subscribe(values => {
        var isAlive: boolean = values.json() as boolean;
        this.connStatus = isAlive ? ConnectionStatus.connected : ConnectionStatus.noConnection;
      });
   }
}
