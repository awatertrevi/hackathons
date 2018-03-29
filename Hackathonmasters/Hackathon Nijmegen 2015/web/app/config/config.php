<?php

return new \Phalcon\Config(array(
    'database' => array(
        'adapter'     => 'Mysql',
        'host'        => 'localhost',
        'username'    => '',
        'password'    => '',
        'dbname'      => 'Healthpath',
        'charset'     => 'utf8',
    ),
    'application' => array(
        'controllersDir' => __DIR__ . '/../../app/controllers/',
        'modelsDir'      => __DIR__ . '/../../app/models/',
        'migrationsDir'  => __DIR__ . '/../../app/migrations/',
        'viewsDir'       => __DIR__ . '/../../app/views/',
        'pluginsDir'     => __DIR__ . '/../../app/plugins/',
        'vendorDir'      => __DIR__ . '/../../app/vendors/',
        'filesDir'       => __DIR__ . '/../../app/files/',
        'cacheDir'       => __DIR__ . '/../../app/cache/',
        'routesDir'      => __DIR__ . '/../../app/routes/',
        'publicDir'      => __DIR__ . '/../../public/',
        'baseUri'        => '/',
    ),
    'email' => array(
        'admin' => 'luke@paradoxis.nl',
        'noreply' => 'noreply@paradoxis.nl'
    ),
    'debug' => true
));
