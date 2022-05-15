import { Component, OnInit, TemplateRef, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../models/Customer';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit, OnDestroy {

  public modalRef: BsModalRef;
  public customerForm: FormGroup;
  public title = 'Customers';
  public customerSelect: Customer;
  public textSimple: string;

  private unsubscriber = new Subject();

  public customers: Customer[];
  public customer: Customer;
  public msnDeleteCustomer: string;
  public modeSave = 'post';

  constructor(
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    this.createForm();
  }

  ngOnInit() {
    this.loadCustomers();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }

  createForm() {
    this.customerForm = this.fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      email: ['', Validators.required]
    });
  }

  saveCustomer() {
    if (this.customerForm.valid) {
      this.spinner.show();

      if (this.modeSave === 'post') {
        this.customer = { ...this.customerForm.value };
      } else {
        this.customer = { id: this.customerSelect.id, ...this.customerForm.value };
      }

      this.customerService[this.modeSave](this.customer)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          () => {
            this.loadCustomers();
            this.toastr.success('Customer registered successfully!');
          }, (error: any) => {
            this.toastr.error(`Error: Customer not registered!`);
            console.error(error);
          }, () => this.spinner.hide()
        );

    }
  }

  loadCustomers() {
    const id = +this.route.snapshot.paramMap.get('id');

    this.spinner.show();
    this.customerService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((customers: Customer[]) => {
        this.customers = customers;
        this.toastr.success('Customers have been successfully loaded!');
      }, (error: any) => {
        this.toastr.error('Error: Customers not loaded!');
        console.log(error);
      }, () => this.spinner.hide()
      );
  }

  customerSelect(customer: Customer) {
    this.modeSave = 'put';
    this.customerSelect = customer;
    this.customerForm.patchValue(customer);
  }

  customerNew(customer: Customer) {
    this.customerSelect = new Customer();
    this.customerForm.patchValue(this.customerSelect);
  }

  back() {
    this.customerSelect = null;
  }

}
