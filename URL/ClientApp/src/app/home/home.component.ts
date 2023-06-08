import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  originalUrl: any;
  shortUrl: any ;

  constructor(private http: HttpClient) { }

  shortenUrl()
  {
    debugger
    console.log("enter here" + this.originalUrl);
     let data = { OriginalUrl: this.originalUrl };

    const urlobject = new URL(this.originalUrl);

    this.http.post<string>('https://localhost:7180/api/Url/ShortenUrl', data).subscribe(result => {
      const presponse = JSON.stringify(result)
      this.shortUrl = presponse.substring(13,presponse.length-2)
      //const presponse = JSON.parse(result);
      //this.shortUrl = presponse.shortUrl
      console.log("hhhhhelppppp" + presponse);
    });
    //this.http.post<string>('https://localhost:7180/api/Url/ShortenUrl', data).subscribe
    //  (result => {
    //    console.log("hhhhhelppppp" + JSON.parse(result));
    //    this.shortUrl = result;
    //  });

   // console.log("Output" + this.shortUrl["shortUrl"]);

  }
  redirectToOrginalUrl() {
    debugger

    const data1 = { shortUrl: this.shortUrl };
    //this.http.post('https://localhost:7180/api/Url/GetLongUrl', data1, { responseType: 'text', observe: 'response' }).subscribe(

    //  response => {
    //    const r = "output here" + JSON.stringify(response);
    //    const redirectUrl = response.headers.get('Location');
    //    if (redirectUrl) {
    //      window.location.href = redirectUrl;
    //    }

        this.http.post<any>('https://localhost:7180/api/Url/GetLongUrl', data1).subscribe(

          response => {
            console.log("hhhhhhhhhhhhhhhhhhh" + JSON.stringify(response))
            const r = JSON.stringify(response)
            const originalUrl = JSON.stringify(response.originalUrl);
            console.log("new" + originalUrl)
            window.open(r.substring(12,r.length-2));

          }
        );


    /*  });*/

  }
  
}


