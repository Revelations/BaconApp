//
//  Update.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Update.h"


@implementation Update

-(void)getFile:(NSString *)uP{
    NSString *urlPath = [uP stringByReplacingOccurrencesOfString:@"\n"withString:@""];
    NSLog(@"Jim is in the file drawer:%@", urlPath);
    //[self getDirectory:@"http://revelations.webhop.org:81/log.txt"];
    
    NSURL *url = [NSURL URLWithString:urlPath];
    NSData *urlData = [NSData dataWithContentsOfURL:url];
    
    NSLog(@"Jim has found the file cover");
    
    if(urlData){
        NSLog(@"Jim has found the file");
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        
         NSArray *values = [urlPath componentsSeparatedByString:@"/"];
       // NSLog(@"Hello:%@",[values objectAtIndex:[values count] -1]);
        NSString *backend = [NSString stringWithFormat:@"%@%@", @"/", [values objectAtIndex:[values count] -1]];
        
        
        NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, backend];
        NSLog(@"Writing file to : %@", filePath);
        NSLog(@"Jim is now photocopying");
        [urlData writeToFile:filePath atomically:YES];
        
        //testing
        NSFileManager * filemanager = [NSFileManager defaultManager];
        if([filemanager fileExistsAtPath:filePath]){
            NSLog(@"File does exist");
            int fileSize = [[[NSFileManager defaultManager] attributesOfItemAtPath:filePath error:nil] fileSize];
            NSLog(@"File size is: %d", fileSize);
        }
    }
        else{
            NSLog(@"File Was not created");
        }
    }


-(void)getDirectory:(NSString *)urlPath{
    //http://revelations.webhop.org:81/log.csv
   // NSString *prep = @"http://revelations.webhop.org:81/";
    NSURL *url = [NSURL URLWithString:[NSString stringWithFormat: @"%@%@", urlPath, @"log.txt"]];
    NSData *urlData = [NSData dataWithContentsOfURL:url];
    
    if(urlData){
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        
        
        NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"//log.txt"];
        [urlData writeToFile:filePath atomically:YES];
        
//        NSString *fileString = [NSString stringWithContentsOfFile:aFilePath];
        NSString *fileContents = [NSString stringWithContentsOfFile: filePath];
        //NSString *tmp = [urlData description];
        NSArray *values = [fileContents componentsSeparatedByString:@","];
                
        
        for (NSString *s in values) {
           // NSString *tmp = [s stringByReplacingOccurrencesOfString:@"\\n"withString:@""];
            NSLog(@"URL to request : %@", s);
            [self getFile:[NSString stringWithFormat:@"%@%@", urlPath,s]];
            NSLog(@"Jim is repeating himeself");
            
        }

       //     NSLog(@"File size is: %d", fileSize);
        }
        else{
            NSLog(@"Directory was not created");
        }    
}   




@end
