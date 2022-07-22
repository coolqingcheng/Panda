import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { AccountService } from './api/account.service';
import { CategoryService } from './api/category.service';
import { CommentService } from './api/comment.service';
import { CommentsService } from './api/comments.service';
import { CommonService } from './api/common.service';
import { FriendLinkService } from './api/friendLink.service';
import { HomeService } from './api/home.service';
import { PermissionService } from './api/permission.service';
import { PostService } from './api/post.service';
import { RssService } from './api/rss.service';
import { SiteSettingService } from './api/siteSetting.service';
import { SiteStatisticService } from './api/siteStatistic.service';
import { StatisticService } from './api/statistic.service';
import { TagService } from './api/tag.service';
import { TestService } from './api/test.service';
import { VisitorService } from './api/visitor.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    AccountService,
    CategoryService,
    CommentService,
    CommentsService,
    CommonService,
    FriendLinkService,
    HomeService,
    PermissionService,
    PostService,
    RssService,
    SiteSettingService,
    SiteStatisticService,
    StatisticService,
    TagService,
    TestService,
    VisitorService ]
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
