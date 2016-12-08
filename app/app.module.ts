import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

import { AppComponent }  from './app.component';

import { ProductListComponent } from './products/product-list.component';
import { ProductComponent } from './products/product.component';

import { ProductsService } from './products/products.service';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule
    ],
    declarations: [
        AppComponent,
        ProductListComponent,
        ProductComponent
    ],
    providers: [
        ProductsService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
