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
@class FeedbackController;
@class GameViewController;
@class SettingsViewController;
@class HelpViewController;
@class InfoViewController;

@interface NavigationView : UITableViewController {
    NSArray * cellContent;
    AboutViewController * aboutView;
    ScannerViewController * scannerView;
    InfoViewController * infoView;
    MapViewController * mapView;
    FeedbackController * feedbackView;
    GameViewController * gameView;
    SettingsViewController * settingsView;
    HelpViewController * helpView;
}

#pragma mark - properties

@property(nonatomic, retain) AboutViewController * aboutView;
@property(nonatomic, retain) ScannerViewController * scannerView;
@property(nonatomic, retain) MapViewController * mapView;
@property(nonatomic, retain) FeedbackController * feedbackView;
@property(nonatomic, retain) GameViewController * gameView;
@property(nonatomic, retain) SettingsViewController * settingsView;
@property(nonatomic, retain) HelpViewController * helpView;
@property(nonatomic, retain) InfoViewController * infoView;

@end
