//
//  UpdateController.h
//  BaconApp
//
//  Created by Donoan Hoffman on 26/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface UpdateController : UIViewController {
    IBOutlet UITextView *textInfo;
}
@property(nonatomic,retain) IBOutlet UITextView *textInfo;
-(IBAction)Update:(id)sender;
-(IBAction)NoUpdate:(id)sender;

@end