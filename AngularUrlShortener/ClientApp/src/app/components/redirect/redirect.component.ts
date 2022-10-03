import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Link } from 'src/app/Link';
import { LinksService } from 'src/app/services/links.service';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css']
})
export class RedirectComponent implements OnInit {

  constructor(private router: Router, private linksService: LinksService) { }

  ngOnInit(): void {
    const shortUrl = this.router.url.substring(1);
    if (shortUrl.length == 6) {

      this.linksService.getUrl(shortUrl).subscribe((link) => {
        if (link && link.url) {
          window.location.href = link.url;
          return;
        }
      });
    }

    // Redirect to home.
    // this.router.navigate(['']);
  }

}
