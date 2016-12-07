import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { Product } from './product.model';

const Products : Product[] = [
    {id: 1, product: 'Fly ass shoes', description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed condimentum orci vitae porta ullamcorper. Nunc semper mauris at finibus aliquam. Aenean et urna justo. Phasellus fermentum ex a mattis accumsan. Aenean vel odio mollis, volutpat diam id, interdum nibh. Vivamus mollis consectetur felis dictum sodales. In in ornare mi, a fermentum sapien. Vivamus efficitur neque a felis aliquam bibendum. Fusce vulputate mi tempor arcu rutrum consectetur.', price: 80.00, image: 'product-image.jpg'},
    {id: 2, product: 'Fly ass shoes', description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed condimentum orci vitae porta ullamcorper. Nunc semper mauris at finibus aliquam. Aenean et urna justo. Phasellus fermentum ex a mattis accumsan. Aenean vel odio mollis, volutpat diam id, interdum nibh. Vivamus mollis consectetur felis dictum sodales. In in ornare mi, a fermentum sapien. Vivamus efficitur neque a felis aliquam bibendum. Fusce vulputate mi tempor arcu rutrum consectetur.', price: 80.00, image: 'product-image.jpg'},
    {id: 3, product: 'Fly ass shoes', description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed condimentum orci vitae porta ullamcorper. Nunc semper mauris at finibus aliquam. Aenean et urna justo. Phasellus fermentum ex a mattis accumsan. Aenean vel odio mollis, volutpat diam id, interdum nibh. Vivamus mollis consectetur felis dictum sodales. In in ornare mi, a fermentum sapien. Vivamus efficitur neque a felis aliquam bibendum. Fusce vulputate mi tempor arcu rutrum consectetur.', price: 80.00, image: 'product-image.jpg'}
];

const apiRoute = 'something/products';

@Injectable()
export class ProductsService {
    constructor(private http: Http) { }

    getProducts(): Observable<Product[]> {
        return this.http.get(apiRoute).map((res: Response) => res.json());
    }

    getProductsMock(): Product[] {
        return Products.map(product => this.clone(product));
    }

    private clone(object: any){
        return JSON.parse(JSON.stringify(object));
    }
}