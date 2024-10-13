# Url Shortening Service


## Functional Requirements

- User should be able to create a shortened URL from a long URL.
- User should be able to choose a custom short URL.
- Click to the short URL should redirect the user to the original long URL.
- Users can create custom url with maximum character limit of 16.
- The same long URL must return the same short URL.
- The short URL should support analytics such as the number of redirections from the shortened URL.

## Non-Functional Requirements

- Low Latency: URL redirection should be fast. 
- High Availability: The system should not degrade at any point of time.
- High Scalability: The system should be able to handle large number of requests.

