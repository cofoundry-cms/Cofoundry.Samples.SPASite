import { Component } from '@angular/core';
import { Product } from './product.model';

@Component({
    selector: '[product]',
    template: `
        <img src="product-image">
        <div class="product__details">
            <h3>Product name</h3>
            <p>Product details</p>
            <span>Product Price</span>
        </div>
    `
})

export class ProductComponent {
    
}