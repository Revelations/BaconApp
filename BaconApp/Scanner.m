//
//  Scanner.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Scanner.h"


@implementation Scanner

@synthesize ouputString;

-(void) scan:(UIViewController *) view
{
    ZBarReaderViewController * reader = [ZBarReaderViewController new];
    reader.readerDelegate = self;
    reader.supportedOrientationsMask = ZBarOrientationMaskAll;
    
    [view presentModalViewController:reader animated:YES];
    
    [reader release];
}

-(void) imagePickerController:(UIImagePickerController *)reader didFinishPickingMediaWithInfo:(NSDictionary *)info
{
    id<NSFastEnumeration> results = [info objectForKey:ZBarReaderControllerResults];
    
    ZBarSymbol * symbol = nil;
    
    for(symbol in results)
        break;
    
    ouputString = [symbol data];
    
    [reader dismissModalViewControllerAnimated:YES];
}

@end
