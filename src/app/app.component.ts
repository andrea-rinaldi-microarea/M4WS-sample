import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LoginResponse } from './LoginResponse';

enum ConnectionStatus { connecting, connected, noConnection }
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private _httpClient: HttpClient) { }
  connStatus: ConnectionStatus = ConnectionStatus.connecting;
  connMessages: string = "";
  connectionStatus = ConnectionStatus;
  companies: string[] = [];
  noCompanies: boolean = false;
  company: string = "";
  userName: string = "";
  password: string = "";
  loginErrors: string = null;
  dismissed: boolean = null;
  authToken: string = null;

  ngOnInit() {
      this._httpClient.get('/api/loginManager/isAlive').subscribe(values => {
        var isAlive: boolean = values as boolean;
        this.connStatus = isAlive ? ConnectionStatus.connected : ConnectionStatus.noConnection;
      }, (error) => {
        this.connStatus = ConnectionStatus.noConnection;
        this.connMessages = error.statusText + ' - ' + error._body;
      });
  }

  onUserChanged() {
    this.companies = [];
    this.noCompanies = false;
    if (this.userName == '')
      return;
    let params = new HttpParams().set("user", this.userName)
    this._httpClient.get('/api/loginManager/enumCompanies', {params: params}).subscribe(values => {
      this.companies = values as string[];
      this.noCompanies = this.companies.length == 0;
    }, (error) => {
    });
  }

  onLogin() {
    this.authToken = null;
    this.loginErrors = null;
    this._httpClient.post('/api/loginManager/login', {user: this.userName, password: this.password, company: this.company}).subscribe((response: LoginResponse) => {
      if (response.loginCompactResult != 0) {
        this.loginErrors = 'Login error, code: ' + response.loginCompactResult;
        this.dismissed = false; 
      } else {
        this.authToken = response.authenticationToken;
      }
    }, (error) => {
      this.loginErrors = error;
      this.dismissed = false; 
    });

  }

  onLogout() {
    this._httpClient.post('/api/loginManager/logout', {authenticationToken: this.authToken}).subscribe(() => {
      this.authToken = null;
      this.companies = [];
      this.noCompanies = false;
      this.company = "";
      this.userName = "";
      this.password = "";
    });
  }
}
