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
#import "FeedbackController.h"
#import "GameViewController.h"
#import "SettingsViewController.h"
#import "HelpViewController.h"
#import "InfoViewController.h"
#import "TitleViewController.h"
#import "BaconAppDelegate.h"


@interface NavigationView : UITableViewController <ModalViewControllerDelegate> {
	NSArray * cellContent;
	AboutViewController * aboutView;
	ScannerViewController * scannerView;
	InfoViewController * infoView;
	MapViewController * mapView;
	FeedbackController * feedbackView;
	GameViewController * gameView;
	SettingsViewController * settingsView;
	HelpViewController * helpView;
    TitleViewController * titleView;
	
	ModalViewController * currentViewController;
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
@property(nonatomic, retain) ModalViewController * currentViewController;
@property (nonatomic, retain) TitleViewController * titleView;


@property (nonatomic,retain) IBOutlet UIButton *button;

-(void) showTitleView;


@end
