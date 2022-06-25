import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { AccountService } from './api/account.service';
import { CategoryService } from './api/category.service';
import { CommonService } from './api/common.service';
import { FriendLinkService } from './api/friendLink.service';
import { HomeService } from './api/home.service';
import { PostService } from './api/post.service';
import { RssService } from './api/rss.service';
import { SiteStatisticService } from './api/siteStatistic.service';
import { StatisticService } from './api/statistic.service';
import { TagService } from './api/tag.service';
import { TestService } from './api/test.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    AccountService,
    CategoryService,
    CommonService,
    FriendLinkService,
    HomeService,
    PostService,
    RssService,
    SiteStatisticService,
    StatisticService,
    TagService,
    TestService ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
