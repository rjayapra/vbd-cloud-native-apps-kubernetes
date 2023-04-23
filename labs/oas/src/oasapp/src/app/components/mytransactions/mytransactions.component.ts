import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { AlertService } from '../alert/alert.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AppInsightsService } from 'src/app/app-insights.service';
import { AuthorizationService } from '../login/authorization.service';
@Component({
  selector: 'mytransactions',
  templateUrl: './mytransactions.component.html',
})
@Injectable({
  providedIn: 'root',
})
export class MyTransactionsComponent {
  auctions: any;
  loading: boolean = false;
  userId: string;

  constructor(
    private authorizationService: AuthorizationService,
    private httpClient: HttpClient,
    private alertService: AlertService,
    private appInsights: AppInsightsService
  ) {
    this.userId = this.authorizationService.getLoggedUser().UserId; //this.storage.get("userId");
  }

  ngOnInit() {
    this.getMyTransactions();
  }

  public getMyTransactions() {
    this.loading = true;
    console.log('User Id is ' + this.userId);
    this.httpClient
      .get(environment.auctionAPI + '/auctionsByUserId/' + this.userId)
      .subscribe(
        (res) => {
          this.auctions = res;
          console.log('Active auctions are loaded');
          console.log(res);
        },
        (err) => {
          console.log(err);
          this.alertService.add({ type: 'danger', message: err });
          this.appInsights.instance.trackException(err);
        }
      );
    this.appInsights.instance.trackEvent({ name: 'LoadedMyTransactions' });
    this.loading = false;
  }
}
