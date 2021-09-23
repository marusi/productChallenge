import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

//User Components
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HeaderComponent } from './header/header.component';
//Start here
import { ProductFormComponent } from './product-form/product-form.component';

//Services
import { ProductService } from './services/product.service';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent, HeaderComponent,
    CounterComponent,
    FetchDataComponent, ProductFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule, 
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'products/new', component: ProductFormComponent },
    ]),

  ],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
