# Secure IIS & MVC

1. Make sure web content is on non-system partition.

2. Remove or rename well-known urls.
    * %systemdrive%inetpubAdminScripts
    * %systemdrive%inetpubscriptsIISSamples
    * http://localhost/iissamples
    * http://localhost/iishelp
    * http://localhost/printers
    * http://localhost/iisadmpwd

3. Require a host headers on all sites. Don’t bind http:/*:80 to any site.

4. Disable directory browsing.

5. Set default application pool identity to least privilege principal.

6. Ensure application pools run under unique identities, and unique application pools for different sites.

7. Config anonymous user identity to use application pool identity, this will greatly reduce the number of accounts needed for websites.
    Open applicationHost.config and make sure you set the userName attribute of the anonymousAuthentication tag is set to a blank string.

```xml
    <system.webServer>
        <security>
            <authentication>
                <anonymousAuthentication userName = ""/>
            </authentication>
        </security>
    </system.webServer>
```

8. Configure authentications,

    a. Ensure sensitive site features is restricted to authenticated principals only.

    &lt;system.webServer&gt;&lt;security&gt;&lt;authorization&gt;&lt;remove users=&quot;*&quot; roles=&quot;&quot; verbs=&quot;&quot; /&gt;&lt;add accessType=&quot;Allow&quot; roles=&quot;administrators&quot; /&gt;

    &lt;/authorization&gt;&lt;/security&gt;&lt;/system.webServer&gt;&lt;/configuration&gt;

    b. Require SSL in forms authentications and configure forms authentication to use cookies.

    &lt;pre&gt;&lt;system.web&gt;&lt;authentication&gt;&lt;forms cookieless=&quot;UseCookies&quot; requireSSL=&quot;true&quot; /&gt;&lt;/authentication&gt;&lt;/system.web&gt;&lt;/pre&gt;

    c. Configure cookie protection mode for forms authentication.

    &lt;pre&gt;&lt;system.web&gt;&lt;authentication&gt;&lt;forms cookieless=&quot;UseCookies&quot; protection=&quot;All&quot; /&gt;&lt;/authentication&gt;&lt;/system.web&gt;&lt;/pre&gt;

    d. Never save password in clear format!!
    Asp.net configurations.

    a. Set deployment method to retail, modify machine.config

    &lt;system.web&gt;  &lt;deployment retail=&quot;true&quot; /&gt;&lt;/system.web&gt;

    b. Turn debug off.

    &lt;system.web&gt;&lt;compilation debug=&quot;false&quot; /&gt;&lt;/system.web&gt;&lt;/configuration&gt;

    c. Ensure custom error messages are not off.

    &lt;customErrors mode=&quot;RemoteOnly&quot;/&gt; or &lt;customErrors mode = &quot;On&quot;/&gt;

    d. Ensure failed request tracing is not enabled.

    - Open IIS.

    - Go to Connections pane, select server connection, site, application or directory.

    - In actions pane, click failed request tracing… make sure the checkbox is not checked.

    e. Configure to use cookies mode for session state in web.config

    &lt;system.web&gt;&lt;sessionState cookieless=&quot;UseCookies&quot; /&gt;&lt;/system.web&gt;

    f. Ensure cookies are set with HttpOnly attribute in web.config. This will stop client side script access to cookies.

    &lt;configuration&gt;&lt;system.web&gt;&lt;httpCookies httpOnlyCookies=&quot;true&quot; /&gt;&lt;/system.web&gt;&lt;/configuration&gt;

    g. Set global .NET trust level. Open IIS, in the features view, double click .NET Trust Levels.
    Request filtering & restrictions in web.config, set maxAllowedContentLength, maxUrl, maxQueryStringallowHighBitCharacters (setting to dis-allow non-ASCII characters) & allowDoubleEscaping.

    &lt;system.webServer&gt;&lt;security&gt;&lt;requestFiltering allowHighBitCharacters=&quot;false&quot; allowDoubleEscaping = &quot;false&quot;&gt;&lt;requestLimits maxAllowedContentLength=&quot;30000000&quot; maxUrl=&quot;4096&quot; maxQueryString=&quot;1024&quot; /&gt;&lt;/requestFiltering&gt;&lt;/security&gt;&lt;/system.webServer&gt;&lt;/configuration&gt;

    Disallow unlisted file extensions in web.config.

    &lt;system.webServer&gt;&lt;security&gt;&lt;requestFiltering&gt;&lt;fileExtensions allowUnlisted=&quot;false&quot; &gt;&lt;add fileExtension=&quot;.asp&quot; allowed=&quot;true&quot;/&gt;

    &lt;add fileExtension=&quot;.aspx&quot; allowed=&quot;true&quot;/&gt;&lt;add fileExtension=&quot;.html&quot; allowed=&quot;true&quot;/&gt;&lt;/fileExtensions&gt;&lt;/requestFiltering&gt;&lt;/security&gt;&lt;/system.webServer&gt;&lt;/configuration&gt;

Comments are closed.
