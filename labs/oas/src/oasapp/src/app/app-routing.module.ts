import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from '../app/components/home/home.component';
import { AuctionComponent } from '../app/components/auction/auction.component';
import { AutoRefreshComponent } from '../app/components/timer/timer.component';
import { BidComponent } from '../app/components/bid/bid.component';
import { WinningBidsComponent } from '../app/components/winningbids/winningbids.component';
import { MyTransactionsComponent } from '../app/components/mytransactions/mytransactions.component';
import { FormsModule } from '@angular/forms';
import { ImageService } from '../app/components/image/imageservice.component';
import { AlertComponent } from '../app/components/alert/alert.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { MainComponent } from '../app/components/main/main.component';
import { BidDetailComponent } from '../app/components/biddetail/biddetail.component';
import { SecurityGaurdService } from '../app/services/securitygaurd.service';
import { PaymentComponent } from '../app/components/payment/payment.component';
import { LoginComponent } from '../app/components/login/login.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [SecurityGaurdService],
  },
  {
    path: 'auction',
    component: AuctionComponent,
    canActivate: [SecurityGaurdService],
  },
  { path: 'bid', component: BidComponent, canActivate: [SecurityGaurdService] },
  {
    path: 'mytransactions',
    component: MyTransactionsComponent,
    canActivate: [SecurityGaurdService],
  },
  { path: 'alert', component: AlertComponent },
  { path: 'main', component: MainComponent },
  {
    path: 'biddetail',
    component: BidDetailComponent,
    canActivate: [SecurityGaurdService],
  },
  {
    path: 'winningbids',
    component: WinningBidsComponent,
    canActivate: [SecurityGaurdService],
  },
  {
    path: 'makePayment',
    component: PaymentComponent,
    canActivate: [SecurityGaurdService],
  },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  declarations: [
    MainComponent,
    BidDetailComponent,
    AuctionComponent,
    HomeComponent,
    PaymentComponent,
    BidComponent,
    WinningBidsComponent,
    MyTransactionsComponent,
    ImageService,
    AlertComponent,
    AutoRefreshComponent,
    LoginComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    FormsModule,
    HttpClientModule,
    BrowserModule,
    NgbModule,
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
