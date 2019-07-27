import { Component, Input, OnInit } from '@angular/core';

import { ValidationResult } from '../validation-result';

@Component({
  selector: 'app-validation-message',
  templateUrl: './validation-message.component.html',
  styleUrls: ['./validation-message.component.scss']
})
export class ValidationMessageComponent implements OnInit {

  @Input() public result: ValidationResult;

  constructor() { }

  ngOnInit() {
  }

}
