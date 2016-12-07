import { Component } from '@angular/core';
import { Product } from './product.model';

@Component({
    selector: '[product-list]',
    template: `
        <div product
            *ngFor="let product of products | async"
            [product]="product"></div>
    `
})

export class ProductListComponent {

}