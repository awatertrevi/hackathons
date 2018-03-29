<?php

use \Phalcon\Di\FactoryDefault;
use \Phalcon\Mvc\View;
use \Phalcon\Mvc\Url as UrlResolver;
use \Phalcon\Db\Adapter\Pdo\Mysql as DbAdapter;
use \Phalcon\Mvc\View\Engine\Volt as VoltEngine;
use \Phalcon\Session\Adapter\Files as SessionAdapter;
use \Phalcon\Mvc\Model\Metadata\Memory as MetaDataAdapter;

/**
 * The FactoryDefault Dependency Injector automatically register the right services providing a full stack framework
 */
$di = new FactoryDefault();

/**
 * The URL component is used to generate all kind of urls in the application
 */
$di->set('url', function () use ($config) {
    $url = new UrlResolver();
    $url->setBaseUri($config->application->baseUri);
    return $url;
}, true);

/**
 * Setting up the view component
 */
$di->set('view', function () use ($config) {
	$view = new View();
	$view->setViewsDir($config->application->viewsDir);
	$view->registerEngines(array(
		'.volt' => function ($view, $di) use ($config) {
			$volt = new VoltEngine($view, $di);
			$volt->setOptions(array(
				'compiledPath'          => $config->application->cacheDir,
				'compiledSeparator'     => '_',
				'compileAlways'         => $config->debug

//				'stat'                  => $config->debug
			));

			/**
			 * Strtotime compatability
			 * @param array $parameters
			 */
			$volt->getCompiler()->addFunction('strtotime', function($parameters) {
				return "strtotime($parameters)";
			});

			// Return volt configuration
			return $volt;
		}
	));

	return $view;
}, true);


/**
 * Database connection is created based in the parameters defined in the configuration file
 */
$di->set('db', function () use ($config) {
    return new DbAdapter($config->database->toArray());
});

/**
 * If the configuration specify the use of metadata adapter use it or use memory otherwise
 */
$di->set('modelsMetadata', function () {
    return new MetaDataAdapter();
});

/**
 * Start the session the first time some component request the session service
 */
$di->setShared('session', function () {
    $session = new SessionAdapter();
    $session->start();
    return $session;
});

/**
 * This allows us to call the config from within our controllers
 * @var object
 */
$di->setShared('config', $config);


/**
 * We're going to register a new router
 * @see http://docs.phalconphp.com/en/latest/reference/routing.html
 */
$di->set('router', function () {

	// Here tell the router to not using default routes
	$router = new \Phalcon\Mvc\Router(false);
	$router->setDefaultNamespace('Paradoxis\Controllers');

	// Index page
	$router->notFound([
		'controller' => 'Index',
		'action' => 'notFound'
	]);

	$router->add('/', 'Index::index');

	// Mount all other routes
	$router->mount(new \Paradoxis\Routes\Api());
	$router->mount(new \Paradoxis\Routes\Dashboard());

	// Return the router object
	return $router;
});
