import { Component, Input } from '@angular/core';
import { Product } from './product.model';

@Component({
    selector: 'product-item',
    template: `
        <img src="">
        <div class="product__details">
            <h3>{{product.product}}</h3>
            <p>{{product.description}}</p>
            <span>{{product.price}}</span>
        </div>
    `
})

export class ProductComponent {
    @Input() product: Product;
}