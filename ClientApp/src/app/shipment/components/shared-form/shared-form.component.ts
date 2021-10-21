import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { SharedFormService } from 'src/app/shared/shared-form.service';
import { FormGroup, FormBuilder } from "@angular/forms";

@Component({
  selector: 'app-shared-form',
  templateUrl: './shared-form.component.html',
  styleUrls: ['./shared-form.component.scss']
})
export class SharedFormComponent implements OnInit {
  @Input() parentGroup: FormGroup;
  @Input() header: any;

  constructor(
    private fb: FormBuilder,
    private sharedFormService: SharedFormService
    ) { 
      this.header = this.sharedFormService.sharedForm();
      this.parentGroup = this.fb.group({
        header: this.sharedFormService.sharedForm(),
      })
  }

  ngOnInit(): void {
  }


}
