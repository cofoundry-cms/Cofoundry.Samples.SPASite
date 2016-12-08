import { Component } from '@angular/core';
import { ProductListComponent } from './products/product-list.component';

@Component({
    selector: 'my-app',
    template: `
        <h1>Hello {{name}}</h1>
        <product-list></product-list>
    `,
})

export class AppComponent  {
    name = 'Angular';
}
