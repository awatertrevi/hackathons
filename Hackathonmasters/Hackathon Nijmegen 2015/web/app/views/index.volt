<!DOCTYPE html>
<html lang="en">
    <head>
        {{ partial('partials/meta') }}
        {{ partial('partials/css') }}
        {{ assets.outputCss() }}
        {% if title is defined and title != '' %}
            <title>HealthPath - {{ title|escape }}</title>
        {% else %}
            <title>HealthPath</title>
        {% endif %}
    </head>
    <body>
        {{ content() }}
        {{ partial('partials/scripts') }}
        {{ assets.outputJs() }}
    </body>
</html>
