	//
//  UpdateController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface UpdateController : UIViewController {
    IBOutlet UILabel *firstLabel;
    IBOutlet UILabel *secondLabel;
}

-(IBAction)Update:(id)sender;
-(IBAction)NoUpdate:(id)sender;

@end
