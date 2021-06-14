use std::{ thread, time };
use std::iter;
use rand::{Rng, thread_rng};
use rand::distributions::Alphanumeric;
use chrono::Utc;
use clap::{ crate_version, App };
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::Arc;

// command line params
struct Config {
    iterations: u32,
    sleep: u64,
    run_loop: bool,
    dry_run: bool,
}

fn main() {

    // parse command line
    let config = parse_config();

    // handle --dry-run
    if config.dry_run {
        println!("Iterations: {}", config.iterations);
        println!("Sleep: {}", config.sleep);
        println!("Loop: {}", config.run_loop);
        println!("Dry Run: {}", config.dry_run);
        return;
    }

    let mut index = 0;
    let sleep = time::Duration::from_millis(config.sleep);

    // sig handler
    let running = Arc::new(AtomicBool::new(true));
    let r = running.clone();

    ctrlc::set_handler(move || {
        r.store(false, Ordering::SeqCst);
    }).expect("Error setting Ctrl-C handler");
    
    // write the log messages to stdout / stderr
    while running.load(Ordering::SeqCst) {
        match index % 5 {
            // 500 goes to stderr
            1 => eprintln!("{}", get_log(index)),

            // everything else
            _ => println!("{}", get_log(index))
        }

        index = index + 1;

        // reset the index so it doesn't go out of bounds
        if config.run_loop && index > 4 { index = 0; }

        // end loop after --iterations
        if !config.run_loop && index >= config.iterations { break; }

        // sleep between requests
        if config.sleep > 0 { thread::sleep(sleep); }
    }
}

/// generate fake json log entries
fn get_log(index: u32) -> String {
    let ndx = index % 5;
    let now = Utc::now();
    let res: String;

    // generate 200, 500 and 400 log items
    match ndx {
        1 => res = format!("{{ \"date\": \"{}\", \"statusCode\": {}, \"path\": \"/log/app\", \"duration\": {}, \"message\": \"Server Error\" }}", now.to_rfc3339(), 500, get_duration(500)),
        4 => res = format!("{{ \"date\": \"{}\", \"statusCode\": {}, \"path\": \"/log/app\", \"duration\": {}, \"message\": \"Invalid Parameter\"  }}", now.to_rfc3339(), 400, get_duration(400)),
        _ => res = format!("{{ \"date\": \"{}\", \"statusCode\": {}, \"path\": \"/log/app\", \"duration\": {}, \"value\": \"{}\" }}", now.to_rfc3339(), 200, get_duration(200), get_random_string(8))
    }

    res
}

// get a random alpha-numeric string
fn get_random_string(length: usize) -> String {
    let mut rng = thread_rng();
    let chars: String = iter::repeat(())
            .map(|()| rng.sample(Alphanumeric))
            .map(char::from)
            .take(length)
            .collect();
    chars    
}

// generate a random request duration based on status code
fn get_duration(status: u32) -> u32 {

    let duration: u32;

    match status {
        400 => duration = rand::thread_rng().gen_range(3..12),
        500 => duration = rand::thread_rng().gen_range(84..166),
        _ => duration = rand::thread_rng().gen_range(9..27)
    }

    duration
}

// parse the command line into a Config structure
fn parse_config() -> Config {
    // default values
    let mut iterations: u32 = 1;
    let mut sleep: u64 = 0;
    let mut run_loop = false;
    let mut dry_run = false;

    // parse the command line
    let matches = App::new("LogApp")
        .version(crate_version!())
        .author("bartr <bartr@outlook.com>")
        .about("Logs json to stdout and stderr")
        .arg("-i --iterations=[int] 'Iterations to log'")
        .arg("-s, --sleep=[int] 'Sleep (ms) between iterations'")
        .arg("-l, --loop 'Run in loop (-s > 0)'")
        .arg("-d, --dry-run 'Validates parameters'")
        .get_matches();

    // panic on parse error
    if matches.is_present("iterations"){
        iterations = matches.value_of_t("iterations").unwrap_or_else(|e| e.exit());
    }

    // panic on parse error
    if matches.is_present("sleep"){
        sleep = matches.value_of_t("sleep").unwrap_or_else(|e| e.exit());
    }

    if matches.is_present("loop") {
        run_loop = true;
    }

    if matches.is_present("dry-run") {
        dry_run = true;
    }

    Config { iterations, sleep, run_loop, dry_run }
}
