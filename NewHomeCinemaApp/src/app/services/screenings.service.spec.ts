import { TestBed } from '@angular/core/testing';

import { ScreeningsService } from './screenings.service';

describe('ScreeningsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ScreeningsService = TestBed.get(ScreeningsService);
    expect(service).toBeTruthy();
  });
});
