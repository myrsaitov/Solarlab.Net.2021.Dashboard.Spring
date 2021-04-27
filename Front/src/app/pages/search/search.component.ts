import {AuthService} from '../../services/auth.service';
import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {AdvertisementService} from '../../services/advertisement.service';
import {BehaviorSubject, Observable} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {GetPagedContentResponseModel} from '../../models/advertisement/get-paged-content-response-model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class SearchComponent implements OnInit {
  response$: Observable<GetPagedContentResponseModel>;
  isAuth = this.authService.isAuth;

  private advertisementsFilterSubject$ = new BehaviorSubject({
    pageSize: 10,
    page: 0
  });
  advertisementsFilterChange$ = this.advertisementsFilterSubject$.asObservable();

  constructor(private authService: AuthService,
              private advertisementService: AdvertisementService) {
  }

  ngOnInit() {
      this.response$ = this.advertisementsFilterChange$.pipe(
      switchMap(advertisementsFilter => this.advertisementService.getAdvertisementsList(advertisementsFilter)
      ));
  }

  get advertisementsFilter() {
    return this.advertisementsFilterSubject$.value;
  }

  updateAdvertisementsFilterPage(page) {
    this.advertisementsFilterSubject$.next({
      ...this.advertisementsFilter,
      page
    });
  }
}