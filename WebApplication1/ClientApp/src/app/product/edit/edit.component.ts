import { Component, Input, OnInit } from '@angular/core';
import { ProductModel } from '../../../models/product.model';
import { ProductService } from '../../../Services/product.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styles: []
})
export class EditComponent implements OnInit {

  constructor(private service: ProductService) { }

  @Input() prod: ProductModel;
  ProductId: number;
  ProductDescription: string;


  // When we click on edit, we only need to know the id and description.
  // Id will be hidden from the user. (see html - only productDescription should be shown)
  ngOnInit() {
    this.ProductId = this.prod.productId;
    this.ProductDescription = this.prod.productDescription;
  }


  // To let the user know the description is updated, I've used just an alert window
  // The response should have the changed description in order for us to say
  updateProduct() {
    this.service.updateDescription(this.ProductId, this.ProductDescription).subscribe(res => {
      if (res !== null && res['productDescription'] === this.ProductDescription) {
        alert("Description has been updated")
      }
      else {
        alert("Description did not update");
      }


    })
  }
}
