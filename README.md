# **Gugutoyer**
---
#### _A decidedly over-engineered bot that creates toys that might or might not have been part of our childhood._

###### _...And shove them in social media for our terror_
---
### *How does this run?*

**Gugutoyer.Bot** is the entrypoint project.

### *How does one run this?*

I mean, first of all you need a database, unless you're only releasing Special Editions. In that case, you can remove the whole repository portion...

There are somewhat flaky scripts to create the tables and the user in a MySQL/MariaDB schema available, as well as a _fairly_ flaky CSV containing all words needed.  

Now, for actually running... It _should_ be something like

`dotnet run src/Gugutoyer.Bot/Gugutoyer.Bot.csproj`

### *How about a cronjob?*

Well, why not? do something like this to run it hourly. Included in the cron folder.

Throw it in your `/etc/cron.d/`.

```bash
SHELL=/bin/sh
PATH=/usr/local/sbin:/usr/local/bin:/sbin:/bin:/usr/sbin:/usr/bin

0 * * * * root dotnet /gugu/Gugutoyer/src/Gugutoyer.Bot/bin/Release/net7.0/Gugutoyer.Bot.dll
```

### *What is the license on this thing?*

GNU AGPL v3. Hopefully.