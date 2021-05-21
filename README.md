# Tomatopaste
 	A fast and simple open source movie torrent scraper

# How to Use
	1.) Open your desired torrenting client (Bittorrent, Utorrent, BittorrentWeb, etc.) and configure your default download directory to the directory where you want the movies.
	2.) Then place rotton tomatoes movie list into the prompt. (Should look similar to this: "https://www.rottentomatoes.com/top/bestofrt/?year=2018" without the quotes)
	3.) Press enter and wait for the message that it is completed the scraping process

# Future changes
	1.) Detect and manage bad or invalid urls
	2.) Adjust search queries to get best movie results
	3.) Scrape search results page for movie with highest number of seeds
	4.) Add ability to not add movies already added
	5.) Make more configurable
	6.) Allow ability to use other sources like IMDB lists, or even a bunch of simple titles like a text document
	7.) Clean up code (MUCH NEEDED)
	8.) Make inserting your cookies simpler

# How it works
	Tomato paste gets the rotton tomato movie list url from the user. Then extracts the movie titles from the list and searches for every movie individually on rarbg.to. If/When a movie is found it grabs the first url in the results panel and simply gets the magnet url on the top of the movies page. It is then added to your favorite torrent client to be downloaded.