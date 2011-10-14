    //
//  NavigationView.h
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>

#pragma mark - import statements
@class AboutViewController;
@class ScannerViewController;
@class MapViewController;
@class FeedbackViewController;
@class GameViewController;
@class SettingsController;
@class HelpViewController;
@class InfoViewController;

@interface NavigationView : UITableViewController {
    NSArray * cellContent;
    AboutViewController * aboutView;
    ScannerViewController * scannerView;
    InfoViewController * infoView;
    MapViewController * mapView;
    FeedbackViewController * feedbackView;
    GameViewController * gameView;
    SettingsController * settingsView;
    HelpViewController * helpView;
}

#pragma mark - properties

@property(nonatomic, retain) AboutViewController * aboutView;
@property(nonatomic, retain) ScannerViewController * scannerView;
@property(nonatomic, retain) MapViewController * mapView;
@property(nonatomic, retain) FeedbackViewController * feedbackView;
@property(nonatomic, retain) GameViewController * gameView;
@property(nonatomic, retain) SettingsController * settingsView;
@property(nonatomic, retain) HelpViewController * helpView;
@property(nonatomic, retain) InfoViewController * infoView;

@end
