import { Component, OnInit } from '@angular/core';

import { Product } from './product.model';
import { ProductsService } from './products.service';
import { ProductComponent } from './product.component';

@Component({
    selector: 'product-list',
    template: `
        <product-item
            *ngFor="let product of products"
            [product]="product"></product-item>
    `
})

export class ProductListComponent implements OnInit {
    private products: Product[];

    constructor(
        public productsService: ProductsService) { }

    ngOnInit() {
        this.getProducts();
    }

    getProducts() {
        this.productsService.getProductsMock().then(products => this.products = products);
    }
}