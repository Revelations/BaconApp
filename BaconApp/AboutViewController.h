//
//  AboutViewController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 15/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewController.h"

extern NSString * const facebook;
extern NSString * const website;

@interface AboutViewController : ModalViewController {
}

-(IBAction)btnFacebookPressed:(id)sender;
-(IBAction)btnWebsitePressed:(id)sender;

@end
