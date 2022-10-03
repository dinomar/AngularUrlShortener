import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Link } from 'src/app/Link';

@Component({
  selector: 'app-links-item',
  templateUrl: './links-item.component.html',
  styleUrls: ['./links-item.component.css']
})
export class LinksItemComponent implements OnInit {
  @Input() link!: Link;
  @Output() onRemoveLink: EventEmitter<Link> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {

  }

  onRemove(link: Link) {
    this.onRemoveLink.emit(link);
  }
}
