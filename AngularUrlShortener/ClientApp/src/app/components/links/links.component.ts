import { Component, OnInit } from '@angular/core';
import { Link } from 'src/app/Link';
import { LinksService } from 'src/app/services/links.service';

@Component({
  selector: 'app-links',
  templateUrl: './links.component.html',
  styleUrls: ['./links.component.css']
})
export class LinksComponent implements OnInit {
  links!: Link[];

  constructor(private linksService: LinksService) { }

  ngOnInit(): void {
    this.linksService.getLinks()
      .subscribe((links) => { this.links = links; });
  }

  removeLink(link: Link) {
    this.linksService.removeLink(link).subscribe(() => {
      this.links = this.links.filter(l => l.id !== link.id);
    });
  }
}
