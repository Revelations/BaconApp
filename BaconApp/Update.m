//
//  Update.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Update.h"


@implementation Update

-(void)getFile:(NSString *)urlPath{
    //http://revelations.webhop.org:81/kiwi.html
    
    NSURL *url = [NSURL URLWithString:urlPath];
    NSData *urlData = [NSData dataWithContentsOfURL:url];
    
    if(urlData){
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        
        NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"kiwi.html"];
        [urlData writeToFile:filePath atomically:YES];
        
        //testing
        NSFileManager * filemanager = [NSFileManager defaultManager];
        if([filemanager fileExistsAtPath:filePath]){
            NSLog(@"File does exist");
            int fileSize = [[[NSFileManager defaultManager] attributesOfItemAtPath:filePath error:nil] fileSize];
            NSLog(@"File size is: %d", fileSize);
        }
        else{
            NSLog(@"File Was not created");
        }
    }
}
@end
