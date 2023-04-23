import { Component, OnInit } from '@angular/core';
import { AlertService } from '../alert/alert.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { AppInsightsService } from 'src/app/app-insights.service';

@Component({
  selector: 'bid',
  templateUrl: './bid.component.html',
})
export class BidComponent {
  auctions: any;
  auction: any;
  loading: boolean = false;

  constructor(
    private httpClient: HttpClient,
    private alertService: AlertService,
    private router: Router,
    private appInsights: AppInsightsService
  ) {}

  ngOnInit() {
    this.getActiveBids();
  }

  public rowSelected(auction: any) {
    this.auction = auction;
    console.log(auction);

    this.router.navigate(['/biddetail'], {
      queryParams: { auction: auction.idAuction },
    });
  }

  public getActiveBids() {
    this.loading = true;
    this.httpClient.get(environment.auctionAPI + '/auctions').subscribe(
      (res) => {
        this.auctions = res;
        console.log('Active auctions are loaded');
        console.log(res);
        this.loading = false;
      },
      (err) => {
        console.log(err);
        this.alertService.add({
          type: 'danger',
          message: 'Some error occured, please contact Administrator',
        });
        this.loading = false;
        this.appInsights.instance.trackException(err);
      }
    );
    this.appInsights.instance.trackEvent({ name: 'LoadedActiveBids' });
  }
}
