CHANGELOG
=========

### 0.9.3

* Expose option to retrieve `scoretags` in Leaderboard entries.

### 0.9.2

* Small enhancements to Leaderboard queries.

### 0.9.1

* Escape query params and handle PATCH methods correctly.

### 0.9.0

* Add Shared Storage API.

### 0.8.0

* BREAKING CHANGE: Due to limitations in SimpleJson, some fields in Achievement and Leaderboards had to be renamed so they could be parsed properly.
* Add callback to get payload data from the Ping operation

### 0.7.0

* Add method to update a gamer's nickname.

### 0.6.1

* Fix deserialization of multiplayer entities.

### 0.6.0

* Add methods for the [Advanced Leaderboards](https://gameup.io/blog/2015/07/31/advanced-leaderboards/) feature.
* Add method to deserialise into user defined type with Cloud Storage.
* Add method to make custom API requests.

### 0.5.1

* Fix `SessionClient.Deserialize (string)`.
* Add methods to retrieve (raw) string response from Cloud Storage requests.

### 0.5.0

* Add JSON-based login, create and password reset for GameUp accounts.

### 0.4.1

* Regenerate the SDK DLL.

### 0.4.0

* Add In-App Purchase Verification support.
* Fix use of `new` with `WWWRequest` and `SingletonMonoBehaviour`.

### 0.3.0

* Add Push Notification support.
* Fix callback handler for Achievement responses.

### 0.2.0

* Add turn-based multiplayer features.

### 0.1.1

* Fix a bug in OAuth login requests.

### 0.1.0

* Initial release.