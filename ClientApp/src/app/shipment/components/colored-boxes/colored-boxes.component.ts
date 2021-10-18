import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-colored-boxes',
  templateUrl: './colored-boxes.component.html',
  styleUrls: ['./colored-boxes.component.scss']
})
export class ColoredBoxesComponent implements OnInit {
  @Input() box!: string;
  
  constructor() { }

  ngOnInit(): void {
  }

}
