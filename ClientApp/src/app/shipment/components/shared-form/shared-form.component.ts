import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { SharedFormService } from 'src/app/shared/shared-form.service';
import { FormGroup, FormBuilder } from "@angular/forms";

@Component({
  selector: 'app-shared-form',
  templateUrl: './shared-form.component.html',
  styleUrls: ['./shared-form.component.scss']
})
export class SharedFormComponent implements OnInit {
  @Input() parentGroup: any;
  @Input() header: any;

  constructor(
    private fb: FormBuilder,
    private sharedFormService: SharedFormService
    ) { 
  }

  ngOnInit(): void {
  }


}
