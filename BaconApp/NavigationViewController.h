//
//  NavigationView.h
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewControllerDelegate.h"
#import "ModalViewController.h"
#import "AboutViewController.h"
#import "ScannerViewController.h"
#import "MapViewController.h"
#import "FeedbackViewController.h"
#import "GameViewController.h"
#import "SettingsViewController.h"
#import "HelpViewController.h"
#import "InfoViewController.h"
#import "TitleViewController.h"
#import "UpdateViewController.h"
#import "BaconAppDelegate.h"


@interface NavigationViewController : UIViewController <ModalViewControllerDelegate, UITableViewDelegate, UITableViewDataSource> {
	NSArray * cellContent;
	AboutViewController * aboutView;
	ScannerViewController * scannerView;
	InfoViewController * infoView;
	MapViewController * mapView;
	FeedbackViewController * feedbackView;
	GameViewController * gameView;
	SettingsViewController * settingsView;
	HelpViewController * helpView;
    TitleViewController * titleView;
    UpdateViewController * updateView;
	
	ModalViewController * currentViewController;
}

#pragma mark - properties

@property(nonatomic, retain) AboutViewController * aboutView;
@property(nonatomic, retain) ScannerViewController * scannerView;
@property(nonatomic, retain) MapViewController * mapView;
@property(nonatomic, retain) FeedbackViewController * feedbackView;
@property(nonatomic, retain) GameViewController * gameView;
@property(nonatomic, retain) SettingsViewController * settingsView;
@property(nonatomic, retain) HelpViewController * helpView;
@property(nonatomic, retain) InfoViewController * infoView;
@property(nonatomic, retain) ModalViewController * currentViewController;
@property (nonatomic, retain) TitleViewController * titleView;
@property (nonatomic, retain) UpdateViewController * updateView;
@property (nonatomic, retain) NSArray * cellContent;

-(void) showTitleView;


@end
