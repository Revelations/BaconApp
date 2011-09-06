//
//  TriButtonContoller.h
//  ViewApp
//
//  Created by Russell Fredericks on 3/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface TriButtonContoller : UIViewController {
	IBOutlet UIView *mapview;
}
@property (nonatomic,retain) UIView *mapview;
-(IBAction) scanbuttonpressed:(id) sender;
-(IBAction) infobuttonpressed:(id) sender;
-(IBAction) mapbuttonpressed:(id) sender;
@end
